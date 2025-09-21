import ImageNotSupportedOutlinedIcon from "@mui/icons-material/ImageNotSupportedOutlined";
import ImageOutlinedIcon from "@mui/icons-material/ImageOutlined";
import { Box, IconButton, Modal } from "@mui/material";
import { useState } from "react";

const modalStyle = {
  position: "absolute",
  top: "50%",
  left: "50%",
  transform: "translate(-50%, -50%)",
  display: "flex",
  justifyContent: "center",
  width: 400,
  bgcolor: "background.paper",
  boxShadow: 24,
  p: 4,
};

const RowWithImage = ({ imageUrl, children }) => {
  const [open, setOpen] = useState(false);
  const handleOpen = () => setOpen(true);
  const handleClose = () => setOpen(false);

  return (
    <>
      <Modal
        open={open}
        onClose={handleClose}
        aria-labelledby="modal-modal-title"
        aria-describedby="modal-modal-description"
      >
        <Box sx={modalStyle}>
          <img
            src={imageUrl}
            style={{ width: 400, height: 200, objectFit: "contain" }}
          />
        </Box>
      </Modal>

      {children}
      <td>
        {imageUrl !== "" ? (
          <IconButton sx={{ padding: 0 }} onClick={handleOpen}>
            <ImageOutlinedIcon />
          </IconButton>
        ) : (
          <IconButton sx={{ padding: 0 }} disabled>
            <ImageNotSupportedOutlinedIcon />
          </IconButton>
        )}
      </td>
    </>
  );
};

export default RowWithImage;
