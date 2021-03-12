<h2><b>DropBox File Manger</b></h2>

<p>
    This Application has configured 0Auth Login with DropBox Authentication server and then access loggd user resources using DropBox resource server.
</p>

<h5><b>Technologies</b></h5>
<ul>
    <li>Asp.net MVC</li>
    <li>DropBox Api V2</li>    
</ul>


<h5><b>Functionalities</b></h5>
<ol>
    <li>Login to Dropbox Account</li>
    <li>View Available File list</li>
    <li>Upload New File</li>
    <li>Delete Available file</li>
    <li>Preview Files with DropBox End point</li>
    <li>Download a copy of the File</li>
    <li>Revoke DropBox Access to Applicatin(Using DropBox User Logout)</li>
</ol>

<h5><b>Steps To Run</b></h5>
<ol>
    <li>Visit https://www.dropbox.com/developers/apps?_tk=pilot_lp&_ad=topbar4&_camp=myapps</li>
    <li>Create new Application with full access requirements</li>
    <li>Redirect URIs as "https://localhost:44304/DropBox/Details"</li>
    <li>Grab App key and  Replace value on Webconfig  "DropBoxAppKey" </li>
    <li>Visit https://www.dropbox.com/developers/documentation/http/documentation#auth-token-from_oauth1 and Search for "get app key and secret" and click on the word and grab the secret.</li>
    <li>Replace value on Webconfig  "AuthApiSecret" in AppSetting</li>
    <li>Run the solution, Enjoy coding!! </li>
    
</ol>

<h5><b>Screen Shots</b></h5>

![loginDrop](https://user-images.githubusercontent.com/65180594/110933470-d3dd8880-8352-11eb-86f1-064b3115c792.PNG)
![DropLogin2](https://user-images.githubusercontent.com/65180594/110933485-d93ad300-8352-11eb-9ea5-56c4a027b2c5.PNG)
![DetailsDrop](https://user-images.githubusercontent.com/65180594/110933503-de981d80-8352-11eb-8d1e-8e189a8cefa9.PNG)
![AddDrop](https://user-images.githubusercontent.com/65180594/110933527-e5bf2b80-8352-11eb-9c75-a704fe87dee3.PNG)
![DeleteDrop](https://user-images.githubusercontent.com/65180594/110933542-e8218580-8352-11eb-8ecf-362007292182.PNG)
![AboutDrop](https://user-images.githubusercontent.com/65180594/110933559-ec4da300-8352-11eb-90db-04735e69006c.PNG)

