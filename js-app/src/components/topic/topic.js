import React, { useState, useEffect } from "react";
import { Card, CardBody, Button } from 'reactstrap';
import { getAllTopics } from "../../modules/topicManager";
import { Link, useNavigate } from "react-router-dom";



const Topic = ({ topic }) => {
    const navigate = useNavigate();
    const [topics, setTopic] = useState([]);
    useEffect(() => {
        getAllTopics().then((res) => { setTopic(res) })
    }, [])



    return <>
    <br></br>
        {/* <div className="selectTopic">Select A Topic</div>   */}
        <div className="Choose-topic">
            
        {topics.map((topic) => (
            <>
            <Link key={topic.id} to={`/videos/${topic.id}`}>
                {topic.title}
            </Link> <br></br>
            <br></br>
            </>
        ))}
        </div>      
    </>
};

export default Topic;