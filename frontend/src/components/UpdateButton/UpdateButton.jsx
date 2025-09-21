import { blue } from "@mui/material/colors";
import { styled } from "@mui/material/styles";
import { IconButton } from "@mui/material";

const UpdateButton = styled(IconButton)(({ theme }) => ({
  width: "56px",
  height: "56px",
  borderRadius: 5,
  color: "white",
  backgroundColor: blue["700"],
  "&:hover": {
    backgroundColor: blue["800"],
  },
}));

export default UpdateButton;
