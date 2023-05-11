import { React, useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { addVideo } from '../../modules/videoManager';
import { getAllTopics } from '../../modules/topicManager';
import { getAllTags } from '../../modules/tagManager';

const VideoForm = ({ getVideos }) => {
    const navigate = useNavigate();
    const [video, setVideo] = useState(
        {
            title: "",
            description: "",
            url: "",
            topics: [],
            selectedTags: []
        }
    )
    const [tags, setTags ] = useState([]);
    const [topics, setTopics ] = useState([]);
    useEffect(() => {
        getAllTags().then((res) => {setTags(res) })
        getAllTopics().then((res) => {setTopics(res) })
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
                        onChange={
                            (evt) => {
                                const copy = { ...video };
                                copy.topics = parseInt(evt.target.value);
                                setVideo(copy);
                            }
                        }>
                        <option value="">Select a Topic</option>
                        {topics.map((topic) => (
                            <option key={topic.id} value={topic.id}>
                                {topic.title}
                            </option>
                        ))}
                    </select>
                    <select                        
                        onChange={
                            (evt) => {
                                const copy = { ...video };
                                copy.tags = parseInt(evt.target.value);
                                setVideo(copy);
                            }
                        }>
                        <option value="">Select a Tag</option>
                        {tags.map((tag) => (
                            <option key={tag.id} value={tag.id}>
                                {tag.name}
                            </option>
                        ))}
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