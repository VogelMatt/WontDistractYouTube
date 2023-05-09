import React from "react";
import { Card, CardBody, ListGroup,ListGroupItem } from "reactstrap";
import { Link } from "react-router-dom";

const Video = ({ video }) => {
    return (
    <Card className="mt-3">
      
      <CardBody>
        <iframe className="video"
          src={video.url}
          title="YouTube video player"
          frameBorder="0"
          allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture"
          allowFullScreen />

        <p>
        <Link to={`/videos/${video.id}`}>
            <strong>{video.title}</strong>
          </Link>
          <p className="text-left px-2">Posted by: {video.userProfile.displayName}</p>
        </p>
        <p>{video.info}</p>
        <ListGroup>
          {video?.comments?.map((c) => (
            <ListGroupItem>{c.message}</ListGroupItem>
          ))}
        </ListGroup>          
      </CardBody>
    </Card>
  );
};

export default Video;