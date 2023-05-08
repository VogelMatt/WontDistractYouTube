import React, { useEffect, useState } from "react";
import Video from './Video';
import { getAllVideos } from "../modules/videoManager";
import VideoForm from './VideoForm';

export const VideoList = ({searchResults}) => {
  const [videos, setVideos] = useState([]);
  const [filteredVideos, setFilteredVideos] = useState([]);

  const getVideos = () => {
    getAllVideos().then(videos => setVideos(videos));
  };

  useEffect(() => {
    getVideos();
  }, []);

  useEffect(
    () => {
        const searchedVideos = videos.filter(video =>
            video.title.toLowerCase().includes(searchResults.toLowerCase()))
        setFilteredVideos(searchedVideos)
    },
    [ searchResults, videos ]
  )

  return (
    <>
        <VideoForm getVideos={getVideos}/>
        <div className="container">
          <div className="row justify-content-center">
            {filteredVideos.map((video) => (
              <Video video={video} key={video.id} />
            ))}
          </div>
        </div>
    </>
  );
}

export default VideoList;