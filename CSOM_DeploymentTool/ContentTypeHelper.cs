using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSOM_DeploymentTool
{
    class ContentTypeHelper
    {
        public static void CreateContentType()
        {
            var clientContext = Helper.GetClientContext();
            Web oWeb = clientContext.Web;

            oWeb.ContentTypes.Add(new ContentTypeCreationInformation
            {
                Name = "Content Type Created By CSOM_WithoutRef",
                Group= "SharePoint Saturday 2014 Content Types"
            });
            clientContext.ExecuteQuery();

            // create by reference
            var itemContentTypes = clientContext.LoadQuery(oWeb.ContentTypes.Where(ct => ct.Name == "Item"));
            clientContext.ExecuteQuery();

            var itemContentType = itemContentTypes.FirstOrDefault();

            if (itemContentType != null)
            {
                oWeb.ContentTypes.Add(new ContentTypeCreationInformation
                {
                    Name = "Content Type Created By CSOM_WithRef",
                    Group = "SharePoint Saturday 2014 Content Types",
                    ParentContentType = itemContentType
                });
                clientContext.ExecuteQuery();
            }
            else
            {
                throw new InvalidOperationException("Item Content Type not found");
            }
        }

        public static void AddExistingSiteColumnToContentType()
        {
            var clientContext = Helper.GetClientContext();
            Web oWeb = clientContext.Web;

            Field age = oWeb.Fields.GetByInternalNameOrTitle("Age_SC");
            Field sessionName = oWeb.Fields.GetByInternalNameOrTitle("SessionName_SC");
            Field sessionPresenter = oWeb.Fields.GetByInternalNameOrTitle("SessionPresenter_SC");

            string contentTypeID = GetContentTypeIDByName("Content Type Created By CSOM_WithRef");
            ContentType contentTypeCreatedByCSOM_WithRef = oWeb.ContentTypes.GetById(contentTypeID);

            FieldLinkCreationInformation dd = new FieldLinkCreationInformation();
            dd.Field = age;
            dd.Field = sessionName;
            dd.Field = sessionPresenter;

            contentTypeCreatedByCSOM_WithRef.FieldLinks.Add(dd);

            contentTypeCreatedByCSOM_WithRef.Update(true);
            clientContext.ExecuteQuery();

        }
        public static void RemoveSiteColumnFromContentType()
        {

            var clientContext = Helper.GetClientContext();
            Web oWeb = clientContext.Web;

            string contentTypeID = GetContentTypeIDByName("Content Type Created By CSOM_WithRef");
            ContentType contentTypeCreatedByCSOM_WithRef = oWeb.ContentTypes.GetById(contentTypeID);
            Guid fieldID = GetSiteColumnIDByName("Age_SC");

            var fieldLinks = contentTypeCreatedByCSOM_WithRef.FieldLinks;
            var fieldLinkToRemove = fieldLinks.GetById(fieldID);
            fieldLinkToRemove.DeleteObject();
            contentTypeCreatedByCSOM_WithRef.Update(true); //push changes
            clientContext.ExecuteQuery();

        }

        public static Guid GetSiteColumnIDByName(string SiteColumnName)
        {
            var clientContext = Helper.GetClientContext();
            Web oWeb = clientContext.Web;

            var siteColumn = clientContext.LoadQuery(oWeb.Fields.Where(sc => sc.InternalName == SiteColumnName));
            clientContext.ExecuteQuery();
            var ageSiteColumn = siteColumn.FirstOrDefault();
            var ageSiteColumnId = ageSiteColumn.Id.ToString();

            return new Guid(ageSiteColumnId);
        }

        public static string GetContentTypeIDByName(string contentTypeName)
        {
            var clientContext = Helper.GetClientContext();
            Web oWeb = clientContext.Web;

            var itemContentType = clientContext.LoadQuery(oWeb.ContentTypes.Where(ct => ct.Name == contentTypeName));
            clientContext.ExecuteQuery();
            var sessionContentType = itemContentType.FirstOrDefault();
            var contentTypeId = sessionContentType.Id.ToString();

            return contentTypeId;
        }

    }
}
