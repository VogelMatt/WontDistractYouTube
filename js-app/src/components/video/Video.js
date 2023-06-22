import React from "react";
import { Card, CardBody } from "reactstrap";
import { Link } from "react-router-dom";

const Video = ({ video }) => {
  return <>

    <Card className="mt-3">

      <CardBody>
      <iframe width="560" height="315" src={`https://www.youtube.com/embed/${video.url}?rel=0controls=1&modestbranding=1`} frameborder="0"></iframe>      
        <p>          
            <strong>{video.title}</strong>        
          <p className="text-left px-2">Posted by: {video.userProfile.displayName}</p>
        </p>
        <p>{video.info}</p>
      </CardBody>
    </Card>
  </>
};

export default Video;


