import { React, useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { addVideo } from '../../modules/videoManager';
import { getAllTopics } from '../../modules/topicManager';
import { getAllTags } from '../../modules/tagManager';
import { Button, Form, FormGroup, Label, Input } from "reactstrap";

const VideoForm = () => {
    const navigate = useNavigate();
    const [video, setVideo] = useState(
        {
            title: "",
            info: "",
            url: "",
            topics: [],
            selectedTags: []
        }
    )
    const [tags, setTags] = useState([]);
    const [topics, setTopics] = useState([]);
    useEffect(() => {
        getAllTags().then((res) => { setTags(res) })
        getAllTopics().then((res) => { setTopics(res) })
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
            <h3 className='addVideo'>
                Add A Video
            </h3>
            <Form className='AddVideoForm'>
                <FormGroup className='form-group'>
                    <Label htmlFor="title">Title</Label>
                    <Input
                        id="title"
                        value={video.title}
                        placeholder="Enter Title..."
                        onChange={evt => {
                            const copy = { ...video }
                            copy.title = evt.target.value
                            setVideo(copy)
                        }}
                    />
                </FormGroup>
                <FormGroup className='form-group'>
                    <Label htmlFor="info">Video Info</Label>
                    <Input
                        id="info"
                        value={video.info}
                        placeholder="Enter Video Info..."
                        onChange={evt => {
                            const copy = { ...video }
                            copy.info = evt.target.value
                            setVideo(copy)
                        }}
                    />
                </FormGroup>
                <FormGroup className='form-group'>
                    <Label htmlFor="url">YouTube URL/Id</Label>
                    <Input
                        id="url"
                        value={video.url}
                        placeholder="Enter YouTube URL..."
                        onChange={evt => {
                            const copy = { ...video }
                            copy.url = evt.target.value
                            setVideo(copy)
                        }}
                    />
                </FormGroup>
                <FormGroup className='form-group'>
                    <Label htmlFor="topic">Topic</Label>
                    <Input type="select" id="topic" onChange={evt => {
                        const copy = { ...video }
                        copy.topicId = parseInt(evt.target.value)
                        setVideo(copy)
                    }}>
                        <option value="">Select a Topic</option>
                        {topics.map((topic) => (
                            <option key={topic.id} value={topic.id}>
                                {topic.title}
                            </option>
                        ))}
                    </Input>
                </FormGroup>
                <FormGroup className='form-group'>
                    <Label htmlFor="tag">Tag</Label>
                    <Input type="select" id="tag" onChange={evt => {
                        const copy = { ...video }
                        copy.tagId = parseInt(evt.target.value)
                        setVideo(copy)
                    }}>
                        <option value="">Select a Tag</option>
                        {tags.map((tag) => (
                            <option key={tag.id} value={tag.id}>
                                {tag.name}
                            </option>
                        ))}
                    </Input>
                </FormGroup>
                <FormGroup className='form-group'>
                    <Button color='success' onClick={handleSaveButtonClick}>Submit</Button>
                </FormGroup>
            </Form>
        </>
    );

}

export default VideoForm




// return (
//     <>
//         <h3 className='addVideo'>Add A Video</h3>
//         <form>
//             <div  className='AddVideoForm'>
//                 <input
//                     className='titleInputAdd'
//                     value={video.title}
//                     placeholder="Enter Title..."
//                     onChange={
//                         (evt) => {
//                             const copy = { ...video }
//                             copy.title = evt.target.value
//                             setVideo(copy)
//                         }
//                     }>
//                 </input>
//                 <input
//                     value={video.info}
//                     placeholder="Enter Video Info..."
//                     onChange={
//                         (evt) => {
//                             const copy = { ...video }
//                             copy.info = evt.target.value
//                             setVideo(copy)
//                         }
//                     }>
//                 </input>
//                 <input
//                     value={video.url}
//                     placeholder="Enter YouTube URL..."
//                     onChange={
//                         (evt) => {
//                             const copy = { ...video }
//                             copy.url = evt.target.value
//                             setVideo(copy)
//                         }
//                     }>
//                 </input>
//                 <select
//                     onChange={
//                         (evt) => {
//                             const copy = { ...video };
//                             copy.topicId = parseInt(evt.target.value);
//                             setVideo(copy);
//                         }
//                     }>
//                     <option value="">Select a Topic</option>
//                     {topics.map((topic) => (
//                         <option key={topic.id} value={topic.id}>
//                             {topic.title}
//                         </option>
//                     ))}
//                 </select>
//                 <select
//                     onChange={
//                         (evt) => {
//                             const copy = { ...video };
//                             copy.tagId = parseInt(evt.target.value);
//                             setVideo(copy);
//                         }
//                     }>
//                     <option value="">Select a Tag</option>
//                     {tags.map((tag) => (
//                         <option key={tag.id} value={tag.id}>
//                             {tag.name}
//                         </option>
//                     ))}
//                 </select>
//             </div>
//             <div>
//                 <button onClick={(clickEvent) => handleSaveButtonClick(clickEvent)}>
//                     Submit
//                 </button>
//             </div>
//         </form>
//     </>
// )