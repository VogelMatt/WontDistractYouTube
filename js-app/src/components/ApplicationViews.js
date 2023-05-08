import React from "react";
import { Routes, Route, Navigate } from "react-router-dom";
import VideoContainer from "./VideoContainer";
import VideoForm from "./VideoForm";
import Login from "./Login";
import Register from "./Register";

const ApplicationViews = ({ isLoggedIn }) => {
  return (
    <Routes>
      <Route path="/" >
        <Route index element={isLoggedIn ? <VideoContainer/> : <Navigate to="/login" />} />
        <Route path="videos">
          <Route index element={isLoggedIn ? <VideoContainer/> : <Navigate to="/login" />} />
          <Route path="add" element={isLoggedIn ? <VideoForm/> : <Navigate to="/login" />} />
          <Route path=":id" element={<p>TODO: Make Video Details component</p>} />
        </Route>
      </Route>
      <Route path="login" element={<Login />} />
      <Route path="register" element={<Register />} />
      <Route path="*" element={<p>Whoops, nothing here...</p>} />
    </Routes>
  );
};

export default ApplicationViews;