const baseUrl = '/api/video';

export const getAllVideos = () => {
  return fetch(baseUrl)
    .then((res) => res.json())
};

export const addVideo = (video) => {
  return fetch(baseUrl, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(video)
  });
};

export const getAllVideosByTopicId = (id) => {
  return fetch (`${baseUrl}/topic/${id}`)
  .then((res) => res.json())
};

export const deleteVideo = (id) => {
  return fetch(`${baseUrl}/${id}`, {
    method: 'DELETE'
  });
};

export const updateVideo = (video) => {
  return fetch(`${baseUrl}/${video.id}`, {
    method: 'PUT',
    headers: {
      "Content-Type": "application/json"
    },
    body: JSON.stringify(video)
  });
};

// export const searchVideos = (q) => {
//     return fetch(baseUrl + `/search?q=${q}&sortDesc=true`)
//     .then((res) => res.json())
// };