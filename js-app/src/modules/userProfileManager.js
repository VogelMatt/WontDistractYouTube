// import { getToken } from "./authManager";

// const baseUrl = "/api/UserProfile";


// export const getUserDetailsById = (userId) => {
//     return getToken().then(token => {
//         return fetch(`${baseUrl}/details/${userId}`, {
//             method: "GET",
//             headers: {
//                 Authorization: `Bearer ${token}`
//             }
//         })
//             .then(res => res.json())
//     })
// }

// export const editUserProfile = (userObj) => {
//     return getToken().then(token => {
//         return fetch(`${baseUrl}/${userObj.id}`, {
//             method: "PUT",
//             headers: {
//                 Authorization: `Bearer ${token}`,
//                 "Content-Type": "application/json"
//             },
//             body: JSON.stringify({
//                 id: userObj.id,
//                 name: userObj.name,
//                 email: userObj.email,
//                 Name: userObj.displayName
//             })
//         })
//             .then(res => res.json())
//     })
// }

// export const getAllUserProfiles = () => {
//     return getToken().then(token => {
//         return fetch(baseUrl, {
//             method: "GET",
//             headers: {
//                 Authorization: `Bearer ${token}`,
//             }
//         })
//             .then(res => res.json())
//     })
// };