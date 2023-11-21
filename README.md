# Enc Chat
this is a simple chat server and client that uses public-key cryptography

<h3 style="color: #8B8000">Warning</h3>
<p>this app was literally created in 3 days so it's not expected to run in a production environment and the author was not targeting to make it run on production environment</p>

<p>
Is a windows app made using C#, the project consist of 3 main parts the server, the api and the client the idea behind the app is to apply something a little bit different when it comes to socket apps which you might find similar to something like remote procedure call. The client first initates a request with the login credientals to the API (which is a minimal api that uses a sqlite database) then it connects to the server with client email and then the server and the client do a key exchange to exchange the encryption key, then the client can choose a person to chat with.
</p>
<h3>Current supported features:</h3>
<ul>
  <li>Messages are encrypted</li>
  <li>Real time messaages notifications</li>
  <li>Ability to send images (limited to only png and 4 kb images)</li>
</ul>

# User Guide
<ol>
  <li>First you need to launch the API</li>
  <li>Then launch the server</li>
  <li>Then launch as many clients as you wish</li>
  <li>Enter your credentials to login (you can sign up for an account if you haven't done already)</li>
</ol>
