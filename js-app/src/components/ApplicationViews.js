import React from "react";
import { Routes, Route, Navigate } from "react-router-dom";
import VideoContainer from "./video/VideoContainer";
import VideoForm from "./video/VideoForm";
import Login from "./auth/Login";
import Register from "./auth/Register";
import VideoList from "./video/VideoList";

const ApplicationViews = ({ isLoggedIn }) => {
  return (
    <Routes>
      <Route path="/" >
        <Route index element={isLoggedIn ? <VideoList/> : <Navigate to="/login" />} />
        <Route path="videos">
          <Route index element={<VideoList/>} />
          <Route path="add" element={isLoggedIn ? <VideoForm/> : <Navigate to="/login" />} />
          {/* <Route path=":id" element={isLoggedIn ? <UserProfile/> : <Navigate to="/login" />} /> */}
        </Route>
      </Route>
      <Route path="login" element={<Login />} />
      <Route path="register" element={<Register />} />
      <Route path="*" element={<p>Whoops, nothing here...</p>} />
    </Routes>
  );
};

export default ApplicationViews;