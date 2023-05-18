import { useState, useEffect } from "react";
import { useParams } from 'react-router-dom';
import { Table } from "reactstrap";
// import { getAllVideosByTopicId } from "../../modules/videoManager";
import { getUserDetails } from "../../modules/userProfileManager";
import ProfileVideo from "../video/ProfileVideo";

export const UserProfile = () => {
    // const { id } = useParams();
    const [user, setUser] = useState([])
    // const navigate = useNavigate();


    useEffect(() => {
        getUserDetails()
            .then(userData => {
                setUser(userData)
            })
    }, [])
    return (
        <>
            <Table>
                <tbody className="userProfileInfo">          
                        
                        <td>{user.displayName}'s Videos</td>
                        {/* <td>{user.email}</td> */}
                </tbody>
            </Table>
            {!!user.videos ? (
                <section>

                    {user.videos.map((v) =>
                        <ProfileVideo key={v.Id} video={v} />
                    )}
                </section>
            ) : (
                <div />
            )}
        </>
    )
}

export default UserProfile








// import { useState } from 'react';


// const UserProfile() => {
//     const [video, setVideo] = useState({
//         title: '',
//         url: '',
//         info: '',


//         // other video properties...
//     });

//     const { id } = useParams();

//     const handleSubmit = (event) => {
//         event.preventDefault();

//         fetch(`/api/videos/${id}`, {
//             method: 'PUT',
//             headers: {
//                 'Content-Type': 'application/json'
//             },
//             body: JSON.stringify({ Id: id, Video: video })
//         })
//             .then(response => response.json())
//             .then(data => console.log(data))
//             .catch(error => console.error(error));
//     };

//     const handleChange = (event) => {
//         const { name, value } = event.target;
//         setVideo(prevVideo => ({ ...prevVideo, [name]: value }));
//     };

//     return (
//         <form onSubmit={handleSubmit}>
//             <input
//                 type="text"
//                 name="title"
//                 value={video.title}
//                 onChange={handleChange}
//             />
//             <input
//                 type="text"
//                 name="description"
//                 value={video.description}
//                 onChange={handleChange}
//             />
//             {/* other form fields... */}
//             <button type="submit">Submit</button>
//         </form>
//     );
// }