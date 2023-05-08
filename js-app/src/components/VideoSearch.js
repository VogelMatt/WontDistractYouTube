const VideoSearch = ({ searchTerms }) => {
    return (
        <>
        <div className="search-container m-4">
            <form>
                <fieldset>
                    <input
                    type="text"
                    placeholder="Search for a video..."
                    onChange={
                        (event) => {
                            searchTerms(event.target.value)
                        }}/>
                </fieldset>
            </form>
        </div>
        </>
    )
}
export default VideoSearch;