import React, { useState } from "react";
import { Button, Form, FormGroup, Label, Input } from "reactstrap";
import { useNavigate } from "react-router-dom";
import { register } from "../../modules/authManager";

export default function Register() {
  const navigate = useNavigate();

  const [name, setName] = useState();
  const [displayName, setDisplayName] = useState();

  const [email, setEmail] = useState();
  const [password, setPassword] = useState();
  const [confirmPassword, setConfirmPassword] = useState();

  const registerClick = (e) => {
    e.preventDefault();
    if (password && password !== confirmPassword) {
      alert("Passwords don't match. Try harder.");
    } else {
      const userProfile = { name, email, displayName };
      register(userProfile, password).then(() => navigate("/"));
    }
  };

  return (
    
    <Form className="registerForm form" onSubmit={registerClick}>
  <fieldset>
    <FormGroup className="form-group">
      <Label htmlFor="name">Name</Label>
      <Input
        id="name"
        type="text"
        autoFocus
        className="input"
        onChange={(e) => setName(e.target.value)}
      />
    </FormGroup>
    <FormGroup className="form-group">
      <Label htmlFor="displayName">Display Name</Label>
      <Input
        id="displayName"
        type="text"
        autoFocus
        className="input"
        onChange={(e) => setDisplayName(e.target.value)}
      />
    </FormGroup>
    <FormGroup className="form-group">
      <Label htmlFor="email">Email</Label>
      <Input
        id="email"
        type="text"
        className="input"
        onChange={(e) => setEmail(e.target.value)}
      />
    </FormGroup>
    <FormGroup className="form-group">
      <Label htmlFor="password">Password</Label>
      <Input
        id="password"
        type="password"
        className="input"
        onChange={(e) => setPassword(e.target.value)}
      />
    </FormGroup>
    <FormGroup className="form-group">
      <Label htmlFor="confirmPassword">Confirm Password</Label>
      <Input
        id="confirmPassword"
        type="password"
        className="input"
        onChange={(e) => setConfirmPassword(e.target.value)}
      />
    </FormGroup>
    <FormGroup className="form-group">
      <Button className="button">Register</Button>
    </FormGroup>
  </fieldset>
</Form>
  );
}


{/* <Form className="registerForm form" onSubmit={registerClick}>
  <fieldset>
    <FormGroup className="form-group">
      <Label htmlFor="name">Name</Label>
      <Input
        id="name"
        type="text"
        autoFocus
        className="input"
        onChange={(e) => setName(e.target.value)}
      />
    </FormGroup>
    <FormGroup className="form-group">
      <Label htmlFor="displayName">Display Name</Label>
      <Input
        id="displayName"
        type="text"
        autoFocus
        className="input"
        onChange={(e) => setDisplayName(e.target.value)}
      />
    </FormGroup>
    <FormGroup className="form-group">
      <Label htmlFor="email">Email</Label>
      <Input
        id="email"
        type="text"
        className="input"
        onChange={(e) => setEmail(e.target.value)}
      />
    </FormGroup>
    <FormGroup className="form-group">
      <Label htmlFor="password">Password</Label>
      <Input
        id="password"
        type="password"
        className="input"
        onChange={(e) => setPassword(e.target.value)}
      />
    </FormGroup>
    <FormGroup className="form-group">
      <Label htmlFor="confirmPassword">Confirm Password</Label>
      <Input
        id="confirmPassword"
        type="password"
        className="input"
        onChange={(e) => setConfirmPassword(e.target.value)}
      />
    </FormGroup>
    <FormGroup className="form-group">
      <Button className="button">Register</Button>
    </FormGroup>
  </fieldset>
</Form> */}