import "./App.scss";

function Login() {
  return (
    <div className="App">
      <div className="login-container">
        <div className="header">
          <h2 className="text">Login Page</h2>
        </div>
        <div className="inputs">
          <input type="text" name="" id="Email" placeholder="Email" />
          <input type="password" name="" id="Password" placeholder="Password" />
        </div>
        <button className="login-btn">Login</button>
        <p>
          Don't have an account? <a href="/signup">Sign up</a>
        </p>
      </div>
    </div>
  );
}

export default Login;
