import { useState, useEffect } from "react";
import { useParams } from 'react-router-dom';
import { Table } from "reactstrap";

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







