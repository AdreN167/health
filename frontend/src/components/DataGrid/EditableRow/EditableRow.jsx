import { IconButton } from "@mui/material";
import EditOutlinedIcon from "@mui/icons-material/EditOutlined";
import DeleteForeverOutlinedIcon from "@mui/icons-material/DeleteForeverOutlined";

const EdiatableRow = ({ children, id, onEditClick, onDeleteClick }) => {
  return (
    <>
      {children}
      <td>
        <IconButton sx={{ padding: 0 }} onClick={() => onEditClick(id)}>
          <EditOutlinedIcon />
        </IconButton>
        <IconButton
          sx={{ marginLeft: 6, padding: 0 }}
          onClick={() => onDeleteClick(id)}
        >
          <DeleteForeverOutlinedIcon />
        </IconButton>
      </td>
    </>
  );
};

export default EdiatableRow;
