const baseUrl = '/api/topic';


export const getAllTopics = () => {
    return fetch(baseUrl)
    .then((res) => res.json())
};