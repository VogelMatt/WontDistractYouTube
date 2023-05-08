const baseUrl = '/api/video';

export const getTags = () => {
    return fetch(baseUrl + '/GetAllTags')
    .then((res) => res.json())
};
