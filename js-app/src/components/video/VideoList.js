import React, { useEffect, useState } from "react";
import Video from './Video';
import {  getAllVideosByTopicId } from "../../modules/videoManager";
import { useNavigate, useParams } from "react-router-dom"




export default function VideoList() {
  const [videos, setVideo] = useState([]);
  const { topicId } = useParams(),
  [id, setId] = useState(topicId ?? ""),
  
  navigate = useNavigate()
  

  useEffect(() => {
    getAllVideosByTopicId(id).then((res) => { setVideo(res) })
  },[]);

  return (
    <section>
      {videos.map((v) =>
      <Video key={v.Id} video={v}/>
      )}
    </section>
  );
}




