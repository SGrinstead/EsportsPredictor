# EsportsPredictor

<p>EsportsPredictor is an app where you can see information about upcoming professional video game matches, teams, and players.
    When viewing a match, you can make a prediction on who will win. 
    Your predictions page keeps track of all the predictions you have made and updates to show if you were correct or not after the matches take place.</p>
<p>You can find the deployed app <a href="https://esportspredictor.azurewebsites.net/">Here</a></p>
<p>You can view most pages as a guest, but in order to make a prediction you will need to create an account.</p>

## Set Up Your Own Local EsportsPredictor
<details>
  <summary>Setup Instructions</summary>
  
  <h3>Prerequisites</h3>
  <ul>
    <li>Visual Studio</li>
    <li>A PandaScore API key (instructions below)</li>
  </ul>
  
  <h3>Initial Setup</h3>
  <ol>
    <li>Fork this repo in GitHub</li>
    <li>Clone your forked repo and open it in Visual Studio</li>
  </ol>
  
  <h3>API Setup</h3>
  <ol>
    <li>Create a <a href="https://app.pandascore.co/dashboard/main">PandaScore</a> account</li>
    <li>Generate your personal PandaScore API key on the dashboard</li>
    <li>In Visual Studio, right-click the project and select 'Manage User Secrets'</li>
    <img src="https://github.com/SGrinstead/EsportsPredictor/assets/48660896/524db695-c6ed-45aa-8ea5-066f6d620af8">
    <li>Copy the code below into your secrets.json file, pasting your API key (not including 'Bearer ')</li>
    <code>{
  "PandascoreToken": "YOUR PANDASCORE API TOKEN GOES HERE"
}
</code>
    <li>It should look like this:</li>  
    <img src="https://github.com/SGrinstead/EsportsPredictor/assets/48660896/26be207a-160a-4a22-b6ed-44ad62bd1026">
  </ol>

  <h3>Database Setup</h3>
  <p>This app is set up to use a local database inside Visual Studio. If you want to change this, you can change the connection string in appsettings.json</p>
  <p>The necessary migrations for the database are already set up. You can either run the application and click apply migrations when prompted or run the update-database command in the package manager console.</p>

  <h3>Run the App</h3>
  <p>You're all done!</p>

</details>
