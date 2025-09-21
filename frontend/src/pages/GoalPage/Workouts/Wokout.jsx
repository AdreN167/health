import {
  Box,
  Button,
  IconButton,
  Stack,
  styled,
  Typography,
} from "@mui/material";
import DeleteForeverOutlinedIcon from "@mui/icons-material/DeleteForeverOutlined";
import { grey } from "@mui/material/colors";
import AddIcon from "@mui/icons-material/Add";
import UpdateButton from "../../../components/UpdateButton/UpdateButton";
import EditOutlinedIcon from "@mui/icons-material/EditOutlined";
import ExerciseModal from "./Exercises/ExerciseModal";
import { useContext, useEffect, useState } from "react";
import ExerciseSmallCard from "./Exercises/ExerciseSmallCard";
import WorkoutsContext from "../../../store/Workouts/WorkoutsContext";
import InnerShadowBox from "../../../components/InnerShadowBox/InnerShadowBox";
import Timer from "../Timer";
import { createWorkoutEvent } from "../../../api/workoutEventJournalService";
import AuthContext from "../../../store/AuthContext";
import { getFormatedDate } from "../../../common/helpers";
import dayjs from "dayjs";

const AddButton = styled(Button)(({ theme }) => ({
  color: grey[400],
  "&:hover": {
    color: grey[600],
  },
}));

const Workout = ({
  id,
  name,
  addedExercises,
  onUpdate,
  onDelete,
  disabled = false,
}) => {
  const { token } = useContext(AuthContext);

  const [open, setOpen] = useState(false);
  const [scrollPostion, setScrollPosition] = useState(0);

  const handleOpen = () => {
    setScrollPosition(window.scrollY);
    setOpen(true);
  };
  const handleClose = () => {
    setOpen(false);
    window.scrollTo(0, scrollPostion);
  };

  const { addExercises } = useContext(WorkoutsContext);
  const [exercises, setExercises] = useState(addedExercises);

  const handleClickDeleteWorkout = () => {
    onDelete(id);
  };

  const handleClickUpdateWorkout = () => {
    onUpdate(id);
  };

  const handleExerciseInModalClick = (exercise) => {
    const allExercises = [exercise, ...exercises];
    setExercises(allExercises);
    addExercises(id, allExercises); // заменят текущий список новым, а не добавляет к существующему
    setOpen(false);
  };

  const handleDeleteExerciseClick = (exId) => {
    const filteredExercises = exercises.filter((ex) => ex.id !== exId);
    setExercises(filteredExercises);
    addExercises(id, filteredExercises);
  };

  const handleCountChange = (exId, count) => {
    console.log(count);
    const allExercises = exercises.map((ex) =>
      ex.id === exId ? { ...ex, repetitions: count } : ex
    );
    setExercises(allExercises);
    addExercises(id, allExercises);
  };

  const handleTimerStopClick = async () => {
    console.log(id, getFormatedDate(dayjs().toString()));
    await createWorkoutEvent(token, {
      workoutId: id,
      date: getFormatedDate(dayjs().toString()),
    });
  };

  return (
    <>
      <ExerciseModal
        open={open}
        handleClose={handleClose}
        exercisesInUse={exercises.map((ex) => ex.id)}
        onExerciseClick={handleExerciseInModalClick}
      />
      <Box sx={{ width: "100%" }}>
        <Stack direction="row" alignItems="center" sx={{ maxWidth: 250 }}>
          <Stack direction="row" alignItems="center">
            <Typography
              sx={{
                maxWidth: 170,
                marginRight: 2,
                whiteSpace: "nowrap",
                overflow: "hidden",
                textOverflow: "ellipsis",
              }}
            >
              {name}
            </Typography>
            {disabled && <Timer onStopClick={handleTimerStopClick} />}
          </Stack>

          {!disabled && (
            <>
              <Box marginLeft="auto">
                <IconButton onClick={handleClickDeleteWorkout}>
                  <DeleteForeverOutlinedIcon />
                </IconButton>
                <IconButton onClick={handleClickUpdateWorkout}>
                  <EditOutlinedIcon />
                </IconButton>
              </Box>
            </>
          )}
        </Stack>
        {!disabled && (
          <AddButton
            sx={{ transition: 0.2 }}
            variant="text"
            startIcon={<AddIcon sx={{ transition: 0.2 }} />}
            onClick={handleOpen}
          >
            Добавить упражнение
          </AddButton>
        )}
        <Stack
          sx={{
            paddingRight: 2,
            height: 340,
          }}
        >
          <InnerShadowBox
            display="flex"
            flexDirection="row"
            gap={4}
            marginTop={2}
            sx={{
              width: "100%",
              height: "100%",
              borderRadius: 2,
            }}
          >
            {exercises.map((ex) => (
              <ExerciseSmallCard
                disabled={disabled}
                key={ex.id}
                id={ex.id}
                name={ex.name}
                count={Number(ex.repetitions)}
                calories={ex.caloriesBurned}
                imageUrl={ex.trainer?.imageUrl}
                onClick={handleDeleteExerciseClick}
                onCountChange={handleCountChange}
              />
            ))}
          </InnerShadowBox>
        </Stack>
      </Box>
    </>
  );
};

export default Workout;
