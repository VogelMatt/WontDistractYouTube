import React from "react";
import { Card, CardBody } from "reactstrap";
import { Link, useNavigate } from "react-router-dom";
import { deleteVideo } from "../../modules/videoManager";
import { Button, ButtonGroup, Modal, ModalBody, ModalFooter, ModalHeader } from "reactstrap"
import { useState } from "react";


const ProfileVideo = ({ video }) => {

  const [modal, setModal] = useState(false),
    navigate = useNavigate()

  const confirmDelete = () => {
    deleteVideo(video.id)
      .then(res => {
        if (res.ok) {
          setModal(false)
          window.location.reload()
        }
      })
  }



  return <>

    <Card className="mt-3">

      <CardBody>
        <iframe width="560" height="315" src={`https://www.youtube.com/embed/${video.url}?rel=0controls=1&modestbranding=1`} frameborder="0"></iframe>
        <p>
          <strong>{video.title}</strong>

        </p>
        <p>{video.info}</p>
        <Button color="danger" onClick={() => setModal(!modal)}>Delete</Button>
        <Link to={`/profile/${video.id}`}>
                <Button>Edit</Button>
            </Link>
        <Modal isOpen={modal} toggle={() => setModal(!modal)}>
          <ModalHeader toggle={() => setModal(!modal)}>Modal Title</ModalHeader>
          <ModalBody>
            Are you sure you want to delete?
          </ModalBody>
          <ModalFooter>
            <Button onClick={() => confirmDelete()} color="primary">Confirm</Button>
            <Button onClick={() => setModal(!modal)} color="secondary">Cancel</Button>
          </ModalFooter>
        </Modal>
      </CardBody>
    </Card>
  </>
};

export default ProfileVideo;


{/* <ListGroup>
          {video?.comments?.map((c) => (
            <ListGroupItem>{c.message}</ListGroupItem>
          ))}
        </ListGroup>           */}