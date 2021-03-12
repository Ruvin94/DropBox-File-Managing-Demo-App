using Dropbox.Api;
using PhotoApp.Models;
using PhotoApp.Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotoApp.Controllers
{
    public class DropBoxController : Controller
    {
        public string token { get; set; }
        // GET: DropBox
        public ActionResult Index()
        {
            var apiAuth = ConfigurationManager.AppSettings["DropBoxAppKey"].ToString();
            return Redirect("https://www.dropbox.com/oauth2/authorize?client_id="+apiAuth+"&redirect_uri=https://localhost:44304/DropBox/Details&response_type=code");
        }

        // GET: DropBox/Details?code=
        public async System.Threading.Tasks.Task<ActionResult> Details(string code)
        {
            ViewBag.Logout = true;
            if (ValueStore.token == null)
            {
                string tokenCode = code;
                var tokenObj = await new DropBoxService().RetreiveTokenAsync(tokenCode);
                ValueStore.token = tokenObj.access_token;
            }
            
            //var user = await new DropBoxService().GetUserBasicInfo(ValueStore.token);
            var result = await new DropBoxService().GetFilesAndFoldersInfo(ValueStore.token);
            return View(result);
        }

        // GET: DropBox/Create
        public ActionResult Create()
        {
            ViewBag.Logout = true;
            return View();
        }

        // Get: DropBox/Download
        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> Download(string filename, string folder)
        {
            try
            {
                var result =await new DropBoxService().DownloadAFile(ValueStore.token, filename, folder);
                return File(result, System.Net.Mime.MediaTypeNames.Application.Octet, filename);
            }
            catch(Exception exr)
            {
                return View();
            }
        }

        // GET: DropBox/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DropBox/Edit/5
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Create(DropBoxFile  dropBoxFile)
        {
            try
            {             
                string FileName = dropBoxFile.Name; 
                string FileExtension = Path.GetExtension(dropBoxFile.ImageFile.FileName);
 
                FileName =  FileName.Trim() + FileExtension;
                await new DropBoxService().UploadAFile(ValueStore.token, dropBoxFile.ImageFile, FileName);
                return RedirectToAction("Details");
            }
            catch
            {
                return View();
            }
        }

        // GET: DropBox/Delete/5
        public async System.Threading.Tasks.Task<ActionResult> Delete(string path)
        {
            ViewBag.Logout = true;
            var Dropobject = await new DropBoxService().GetParticularFileInfo(ValueStore.token, path);
            return View(Dropobject);
        }

        // POST: DropBox/Delete/5
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Delete(string path, FormCollection collection)
        {
            try
            {
                await new DropBoxService().DeleteAFile(ValueStore.token, path);
                return RedirectToAction("Details");
            }
            catch
            {
                return View();
            }
        }


        public async System.Threading.Tasks.Task<ActionResult> GetPreview(string path)
        {
            
            var result = "https://www.dropbox.com/preview"+path+"?role=personal";
            return Redirect(result);                        
            
        }

        public async System.Threading.Tasks.Task<ActionResult> Logout()
        {
            await new DropBoxService().RevokeTokenAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
