import { useState, useEffect } from "react";
import { useParams } from 'react-router-dom';
import { Table } from "reactstrap";
import { getAllVideosByTopicId } from "../../modules/videoManager";


export const UserProfile = () => {
    const { id } = useParams();
    const [user, setUser] = useState([])
    // const navigate = useNavigate();


    useEffect(() => {
        getAllVideosByTopicId(id)
            .then(userData => {
                setUser(userData)
            })
    }, [])
    return (
        <>
            <Table>
                <tbody>
                    <tr>
                        <th>Name</th>
                        <td>{user.displayName}</td>
                    </tr>                    
                    <tr>
                        <th>Email</th>
                        <td>{user.email}</td>
                    </tr>                  
                    
                </tbody>
            </Table>
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