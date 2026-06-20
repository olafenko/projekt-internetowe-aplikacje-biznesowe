using Firma.Data.Data.CMS;
using Firma.Data.Data.CMS.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Firma.Interfaces.CMS
{
    public interface IPageService
    {

        Task<Page?> GetPageById(int? id);
        Task<IList<Page>> GetAllPagesByPositionAsc();

        Task CreateNewPage(string title, string linkTitle, string content, string shortDescription, int position,string? heroPhotoUrl,PageMenuArea? pageMenuArea,string? controllerName,string? actionName,bool isLinkCta);
        Task UpdatePage(int id,string title, string linkTitle, string content, string shortDescription, int position,string? heroPhotoUrl,PageMenuArea? pageMenuArea,string? controllerName,string? actionName,bool isLinkCta);

        Task DeletePage(int id);

        bool PageExists(int id);

    }
}
