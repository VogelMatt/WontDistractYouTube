import { React, useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { addVideo } from '../modules/videoManager';
import { getTags } from '../modules/tagManager';

const VideoForm = ({ getVideos }) => {
    const navigate = useNavigate();
    const [video, setVideo] = useState(
        {
            title: "",
            description: "",
            url: "",
            topic: [],
            selectedTags: []
        }
    )
    const [tags, setTags ] = useState([]);

    useEffect(() => {
        getTags().then(setTags);
    }, [])

    const handleSaveButtonClick = (e) => {
        e.preventDefault()

        addVideo(video).then(window.alert('Video added!'))
            .then((p) => {
                navigate("/")
            });
    }

    return (
        <>
            <h4>Add A Video</h4>
            <form>
                <div>
                    <input
                        value={video.title}
                        placeholder="Enter Title..."
                        onChange={
                            (evt) => {
                                const copy = { ...video }
                                copy.title = evt.target.value
                                setVideo(copy)
                            }
                        }>
                    </input>
                    <input
                        value={video.info}
                        placeholder="Enter Video Info..."
                        onChange={
                            (evt) => {
                                const copy = { ...video }
                                copy.info = evt.target.value
                                setVideo(copy)
                            }
                        }>
                    </input>
                    <input
                        value={video.url}
                        placeholder="Enter YouTube URL..."
                        onChange={
                            (evt) => {
                                const copy = { ...video }
                                copy.url = evt.target.value
                                setVideo(copy)
                            }
                        }>
                    </input>
                    <select
                        value={video.topicId}
                        onChange={
                            (evt) => {
                                const copy = { ...video };
                                copy.topicId = parseInt(evt.target.value);
                                setVideo(copy);
                            }
                        }>
                        {/* <option value="">Select a Topic</option>
                        {topics.map((topic) => (
                            <option key={topic.id} value={topic.id}>
                                {topic.title}
                            </option>
                        ))} */}
                    </select>
                </div>
                <div>
                    <button onClick={(clickEvent) => handleSaveButtonClick(clickEvent)}>
                        Submit
                    </button>
                </div>
            </form>
        </>
    )
}

export default VideoForm