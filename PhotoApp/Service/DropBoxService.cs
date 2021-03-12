using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Script.Serialization;
using Dropbox.Api;
using Dropbox.Api.Files;
using PhotoApp.Models;

namespace PhotoApp.Service
{
    public class DropBoxService
    {   
        public async System.Threading.Tasks.Task<DropBoxTokenResponse> RetreiveTokenAsync(string tokenCode)
        {
            HttpClient client = new HttpClient();
            var appKey = ConfigurationManager.AppSettings["AuthApiSecret"].ToString();
            client.DefaultRequestHeaders.Add("Authorization", "Basic "+ appKey);
            string url = "https://api.dropbox.com/oauth2/token?code=" + tokenCode + "&grant_type=authorization_code&redirect_uri=https://localhost:44304/DropBox/Details";
            var response = await client.PostAsync(url,null);
            var contents = await response.Content.ReadAsStringAsync();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var tokenObject = serializer.Deserialize<DropBoxTokenResponse>(contents);
            return tokenObject;
        }

        public async System.Threading.Tasks.Task RevokeTokenAsync()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer "+ ValueStore.token);
            string url = "https://api.dropboxapi.com/2/auth/token/revoke";
            await client.PostAsync(url, null);
            ValueStore.token = null;
        }

        public async System.Threading.Tasks.Task<DropBoxFile> GetParticularFileInfo(string token, string path)
        {

            //List<DropBoxFile> objs = new List<DropBoxFile>();
            using (var dbx = new DropboxClient(token))
            {
                var list = await dbx.Files.ListFolderAsync(string.Empty, recursive: true);


                foreach (var item in list.Entries.Where(i => i.IsFolder))
                {
                    Console.WriteLine("D  {0}/", item.Name);
                }

                foreach (var item in list.Entries.Where(i => i.IsFile))
                {
                    Console.WriteLine("F{0,8} {1}", item.AsFile.Size, item.Name);
                    var obj = new DropBoxFile() { Name = item.Name, ModifiedAt = item.AsFile.ClientModified.ToString(), Size = item.AsFile.Size.ToString(), Path = item.AsFile.PathLower };
                    if(obj.Path == path)
                    {
                        return obj;                      
                    }
                }
                return null;
            }
        }

        public async System.Threading.Tasks.Task<List<DropBoxFile>> GetFilesAndFoldersInfo(string token)
        {
            
            List<DropBoxFile> objs = new List<DropBoxFile>();
            using (var dbx = new DropboxClient(token))
            {
                var list = await dbx.Files.ListFolderAsync(string.Empty, recursive: true);

                
                foreach (var item in list.Entries.Where(i => i.IsFolder))
                {
                    Console.WriteLine("D  {0}/", item.Name);
                }

                foreach (var item in list.Entries.Where(i => i.IsFile))
                {
                    Console.WriteLine("F{0,8} {1}", item.AsFile.Size, item.Name);
                    var obj = new DropBoxFile() { Name = item.Name, ModifiedAt = item.AsFile.ClientModified.ToString(), Size = item.AsFile.Size.ToString(), Path = item.AsFile.PathLower };
                    objs.Add(obj);
                }
                return objs;
            }
        }

        public async System.Threading.Tasks.Task<bool> UploadAFile(string token, HttpPostedFileBase  fileobject, string filenamewithExt)
        {
            try
            {
                using (var dbx = new DropboxClient(token))
                {
                    //string file = filepath;
                    string folder = "/Public";
                    string filename = filenamewithExt;
                    string url = "";
                    using (var mem = new MemoryStream())
                    {
                        
                        fileobject.InputStream.CopyTo(mem);
                        mem.Position = 0;
                        var updated = dbx.Files.UploadAsync(folder + "/" + filename, WriteMode.Overwrite.Instance, body: mem);
                        updated.Wait();
                        var tx = dbx.Sharing.CreateSharedLinkWithSettingsAsync(folder + "/" + filename);
                        tx.Wait();
                        url = tx.Result.Url;
                   }
                    Console.Write(url);
                    return true;
                }
            }
            catch(Exception exr)
            {
                return false;
            }
        }


        public async System.Threading.Tasks.Task<byte[]> DownloadAFile(string token, string fileName, string pathtoFile)
        {
            try
            {
                using (var dbx = new DropboxClient(token))
                {
                    string folder = pathtoFile;
                    string file = fileName;

                    var list = await dbx.Files.ListFolderAsync(string.Empty, recursive: true);
                    foreach (var item in list.Entries.Where(i => i.IsFile))
                    {
                        string urlFile = item.AsFile.PathLower;
                        if (String.Concat(pathtoFile) == urlFile)
                        {
                            using (var response = await dbx.Files.DownloadAsync(urlFile))
                            {
                                var s = response.GetContentAsByteArrayAsync();
                                s.Wait();
                                var data = s.Result;
                                return data;
                               
                            }
                        }
                       
                    }
                    //return true;
                    return null;
                }
            }
            catch(Exception exr)
            {
                return null;
            }
        }


        public System.Threading.Tasks.Task<Dropbox.Api.Users.FullAccount> GetUserBasicInfo(string token)
        {
            using (var dbx = new DropboxClient(token))
            {
                var user = dbx.Users.GetCurrentAccountAsync();
                return user;
            }
        }


        public async System.Threading.Tasks.Task<bool> DeleteAFile(string token, string filepath)
        {
            try
            {
                using (var dbx = new DropboxClient(token))
                {
                    var result=await dbx.Files.DeleteV2Async(filepath);
                    return true;
                }
            }
            catch (Exception exr)
            {
                return false;
            }
        }

    }
}