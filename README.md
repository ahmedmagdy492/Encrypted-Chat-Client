# Enc Chat
this is a simple chat server and client that uses public-key cryptography

# About
<p>
Is a windows app made using C#, the project consist of 3 main parts; the server, the api and the client, the idea behind the app is to apply something a little bit different when it comes to socket apps which you might find similar to something like remote procedure call. The client first initates a request with the login credientals to the API (which is a minimal api that uses a sqlite database) it connects to the server with client email and then the server and the client do a key exchange to exchange the encryption key, then the client can choose a person to chat with.
</p>
<h3>Current supported features:</h3>
<ul>
  <li>Messages are encrypted</li>
  <li>Real time messaages notifications</li>
  <li>Ability to send images (limited to only png and 4 kb images)</li>
</ul>

<h3>Things to added</h3>
<ul>
  <li>Now the app is capabable of saving messages on the user computer not on the server</li>
  <li>Now when someone is registered using the api the server will be notified and will add that user to the users list</li>
</ul>

<h3>Things to note</h3>
<ul>
  <li>The server has no information about the messages and how it's being saved</li>
</ul>

<h3>Things to consider</h3>
<ul>
  <li>Windows notifications do nothing when clicked</li>
  <li>The image sending feature is so limited specially when it comes to the technique used behind the scene to send the image (which just encodes the image bytes into base64 format and send it) which does not use any type of compression</li>
  <li>There is no ability for the user to change his profile info</li>
  <li>No datetime associated with each message that is being sent</li>
</ul>

# User Guide
<ol>
  <li>First you need to launch the API</li>
  <li>Then launch the server</li>
  <li>Then launch as many clients as you wish</li>
  <li>Enter your credentials to login (you can sign up for an account if you haven't done already) and then start chatting</li>
</ol>
