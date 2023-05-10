import React from "react";
import { Routes, Route, Navigate, useParams } from "react-router-dom";
import VideoContainer from "./video/VideoContainer";
import VideoForm from "./video/VideoForm";
import Login from "./auth/Login";
import Register from "./auth/Register";
import VideoList from "./video/VideoList";


const ApplicationViews = ({ isLoggedIn }) => {
  return (
    <Routes>
      <Route path="/" >
        {/* route for topicList */}
        <Route index element={isLoggedIn ? <VideoList /> : <Navigate to="/login" />} />        
        <Route path="profile">
          {/* <Route index element={isLoggedIn ? <UserProfile /> : <Navigate to="/login" />} /> */}
          {/* route for "add" videoAddForm */}
          <Route path="add" element={isLoggedIn ? <VideoForm /> : <Navigate to="/login" />} />
          {/* route for "edit" add videoEditForm */}
          <Route path=":id" element={isLoggedIn ? <VideoForm /> : <Navigate to="/login" />} />
        </Route>
        <Route path="videos">
          <Route index path=":topicId" element={isLoggedIn ? <VideoList /> : <Navigate to="/login" />} />
        </Route>
      </Route>      
      <Route path="login" element={<Login />} />
      <Route path="register" element={<Register />} />
      <Route path="*" element={<p>Whoops, nothing here...</p>} />
    </Routes>
  );
};

export default ApplicationViews;