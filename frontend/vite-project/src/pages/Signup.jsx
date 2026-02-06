import "./App.scss";

function Signup() {
  return (
    <div className="App">
      <div className="login-container">
        <div className="header">
          <h2 className="text">Signup Page</h2>
        </div>
        <form>
          <div className="inputs">
            <input type="text" name="" id="" placeholder="Name" />
            <input type="text" name="" id="Email" placeholder="Email" />
            <input
              type="password"
              name=""
              id="Password"
              placeholder="Password"
            />
          </div>
          <button className="login-btn">Signup</button>
        </form>
        <p>
          Already have an account? <a href="/login">Login</a>
        </p>
      </div>
    </div>
  );
}

export default Signup;
