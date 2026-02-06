import { useState } from "react";
import "./App.scss";

function Signup() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [name, setName] = useState("");

  async function handleSignup(e) {
    e.preventDefault();
    try {
      const response = await fetch("http://localhost:8000/api/signup", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ name, email, password }),
      });

      if (response.ok) {
        window.location.href = "/login";
      } else {
        const errorData = await response.json();
        alert(`Signup failed: ${errorData.message}`);
      }
    } catch (error) {
      console.error("Error during signup:", error);
      alert("An error occurred during signup. Please try again.");
    }
  }

  return (
    <div className="App">
      <div className="login-container">
        <div className="header">
          <h2 className="text">Signup Page</h2>
        </div>
        <form onSubmit={handleSignup}>
          <div className="inputs">
            <input
              type="text"
              id=""
              placeholder="Name"
              required
              onChange={(e) => setName(e.target.value)}
            />
            <input
              type="text"
              id="Email"
              placeholder="Email"
              required
              onChange={(e) => setEmail(e.target.value)}
            />
            <input
              type="password"
              id="Password"
              placeholder="Password"
              required
              onChange={(e) => setPassword(e.target.value)}
            />
          <button className="login-btn" type="submit">
            Signup
          </button>
          </div>
        </form>
        <p>
          Already have an account? <a href="/login">Login</a>
        </p>
      </div>
    </div>
  );
}

export default Signup;
