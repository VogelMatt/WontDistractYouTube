import React from "react";
import { Card, CardBody } from "reactstrap";

const Video = ({ video }) => {
    return (
    <Card className="mt-3">
      <p className="text-left px-2">Posted by: {video.userProfile.name}</p>
      <CardBody>
        <iframe className="video"
          src={video.url}
          title="YouTube video player"
          frameBorder="0"
          allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture"
          allowFullScreen />

        <p>
          <strong>{video.title}</strong>
        </p>
        <p>{video.description}</p>
        <hr></hr>
        <p><u>Comments</u></p>
        {video.comments.length === 0 ?
        <p>There are no comments on this video.</p> :
        video.comments.map(comment => <p key={comment.Id}>{comment.message}</p>)}
      </CardBody>
    </Card>
  );
};

export default Video;