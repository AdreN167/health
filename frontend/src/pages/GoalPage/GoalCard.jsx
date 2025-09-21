import { Box, IconButton } from "@mui/material";
import EditOutlinedIcon from "@mui/icons-material/EditOutlined";
import DeleteForeverOutlinedIcon from "@mui/icons-material/DeleteForeverOutlined";
import HoveringCard from "../../components/HoveringCard/HoveringCard";

const GoalCard = ({ content, onClick, onEditClick, onDeleteClick }) => {
  const handleClickEditGoal = (currentGoal) => {
    onEditClick(currentGoal);
  };

  const handleClickDeleteGoal = (currentGoal) => {
    onDeleteClick(currentGoal);
  };

  const handleClickCard = (e) => {
    e.preventDefault();
    onClick(content);
  };

  return (
    <HoveringCard
      onClick={handleClickCard}
      sx={{
        display: "flex",
        flexDirection: "row",
        alignItems: "center",
        padding: "0 28px",
        height: 90,
        maxWidth: "95%",
        margin: "0 auto",
        bgcolor: "#DCDCDC",
        borderRadius: 1,
      }}
      scale={1.01}
    >
      <IconButton
        sx={{ marginRight: 2 }}
        onClick={(e) => handleClickEditGoal(content)}
      >
        <EditOutlinedIcon />
      </IconButton>
      {content.name}
      <IconButton
        sx={{ marginLeft: "auto" }}
        onClick={(e) => {
          e.stopPropagation();
          handleClickDeleteGoal(content);
        }}
      >
        <DeleteForeverOutlinedIcon />
      </IconButton>
    </HoveringCard>
  );
};

export default GoalCard;
