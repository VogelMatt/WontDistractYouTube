import { useState } from "react";
import VideoSearch from "./VideoSearch";
import VideoList from "./VideoList";

const VideoContainer = () => {
    const [searchQuery, setSearchQuery] = useState("")

    return <>
        <VideoSearch searchTerms={setSearchQuery}/>
        <VideoList searchResults={searchQuery}/>
    </>
};
export default VideoContainer;