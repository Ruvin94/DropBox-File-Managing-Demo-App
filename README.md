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
