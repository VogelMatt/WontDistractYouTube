import { React, useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { addVideo, updateVideo } from '../../modules/videoManager';
import { getAllTopics } from '../../modules/topicManager';
import { getAllTags } from '../../modules/tagManager';

const VideoForm = ({ getVideos, formType, video }) => {
    const navigate = useNavigate();
    const [videoData, setVideoData] = useState(
        {
            title: "",
            info: "",
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
        if (formType === "edit") {
            setVideoData(video);
        }
    }, [])  
    


    const handleSaveButtonClick = (e) => {
        e.preventDefault()
        if (formType === "add") {
            addVideo(videoData).then(window.alert('Video added!'))
                .then((p) => {
                    navigate("/")
                });
        } else {
            updateVideo(videoData).then(window.alert('Video updated!'))
                .then((p) => {
                    navigate("/")
                });
        }
    }

    return (
        <>
            <h4>{formType === "add" ? "Add" : "Edit"} A Video</h4>
            <form>
                <div>
                    <input
                        value={videoData.title}
                        placeholder="Enter Title..."
                        onChange={
                            (evt) => {
                                const copy = { ...videoData }
                                copy.title = evt.target.value
                                setVideoData(copy)
                            }
                        }>
                    </input>
                    <input
                        value={videoData.info}
                        placeholder="Enter Video Info..."
                        onChange={
                            (evt) => {
                                const copy = { ...videoData }
                                copy.info = evt.target.value
                                setVideoData(copy)
                            }
                        }>
                    </input>
                    <input
                        value={videoData.url}
                        placeholder="Enter Video URL..."
                        onChange={
                            (evt) => {
                                const copy = { ...videoData }
                                copy.url = evt.target.value
                                setVideoData(copy)
                            }
                        }>
                    </input>
                    {formType === "edit" && (
                        <>
                            <select                        
                                onChange={
                                    (evt) => {
                                        const copy = { ...videoData };
                                        copy.topicId = parseInt(evt.target.value);
                                        setVideoData(copy);
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
                                        const copy = { ...videoData };
                                        copy.tagId = parseInt(evt.target.value);
                                        setVideoData(copy);
                                    }
                                }>
                                <option value="">Select a Tag</option>
                                {tags.map((tag) => (
                                    <option key={tag.id} value={tag.id}>
                                        {tag.name}
                                    </option>
                                ))}
                            </select>
                        </>
                    )}
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