import firebase from "firebase/app";
import "firebase/auth";
import { getToken } from "./authManager";
const baseUrl = "/api/UserProfile";


export const getUserDetailsById = (userId) => {

    return fetch(`${baseUrl}/${userId}`
        .then(res => res.json())
    )
};

export const getUserDetails = () => {
    return getToken().then(token => {
        return fetch(`${baseUrl}/WithVideos`, {
            method: "GET",
            headers: {
                Authorization: `Bearer ${token}`
            }
        }).then(res => res.json())
    })
}
