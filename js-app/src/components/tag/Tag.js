import React, { useState, useEffect } from "react";
import { Card, CardBody, Button } from 'reactstrap';

import { getAllTags } from "../../modules/tagManager";




const Tag = ({ tag }) => {
    const [tags, setTag] = useState([]);
    useEffect(() => {
        getAllTags().then((res) => { setTag(res) })
    }, [])



    return <>

        {
            tags.map(tag => {

                <Card className="mt-3" key={tag.id}>

                    <CardBody>
                        <div>
                            <Button
                                color="primary"
                            >

                            </Button>
                        </div>
                        <p>{tag.name}</p>
                    </CardBody>
                </Card>
            })
        };
    </>
};

export default Tag;