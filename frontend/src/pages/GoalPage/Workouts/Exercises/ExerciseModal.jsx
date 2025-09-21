import { Modal, Box, Grid2, Typography } from "@mui/material";
import ExerciseCard from "./ExerciseCard";
import { useContext, useEffect, useState } from "react";
import StorageContext from "../../../../store/StorageContext";

const style = {
  position: "absolute",
  top: "50%",
  left: "50%",
  transform: "translate(-50%, -50%)",
  width: "80%",
  maxHeight: "80vh",
  overflowY: "auto",
  bgcolor: "background.paper",
  boxShadow: 24,
  p: 4,
};

const defaultCount = 5;

const ExerciseModal = ({
  open,
  exercisesInUse,
  handleClose,
  onExerciseClick,
}) => {
  const { exercises, fetchExercises } = useContext(StorageContext);
  const [exercisesFromStorage, setExercisesFromStorage] = useState([]);
  const [isNothingExercises, setIsNothingExercises] = useState(false);

  useEffect(() => {
    if (exercises.length === 0) {
      const get = async () => {
        setExercisesFromStorage(await fetchExercises());
      };
      get();
      return;
    }
    setExercisesFromStorage(exercises);
  }, [exercises, exercisesFromStorage]);

  useEffect(() => {
    setIsNothingExercises(exercises.length === exercisesInUse.length);
  }, [exercisesInUse]);

  const handleClick = (id) => {
    const exercise = exercisesFromStorage.find((ex) => ex.id === id);
    console.log({ ...exercise, repetitions: defaultCount });
    onExerciseClick({ ...exercise, repetitions: defaultCount });
  };

  return (
    <Modal open={open} onClose={handleClose}>
      <Box sx={style}>
        {!isNothingExercises ? (
          <Grid2 container spacing={4} justifyContent="space-around">
            {exercisesFromStorage &&
              exercisesFromStorage.map((exercise) => {
                if (!exercisesInUse.includes(exercise.id)) {
                  return (
                    <Grid2 item="true" xs={12} sm={6} md={3} key={exercise.id}>
                      <ExerciseCard
                        id={exercise.id}
                        name={exercise.name}
                        description={exercise.description}
                        calories={exercise.caloriesBurned}
                        imageUrl={exercise.trainer?.imageUrl}
                        onClick={handleClick}
                      />
                    </Grid2>
                  );
                }
              })}
          </Grid2>
        ) : (
          <Typography>
            К сожалению, пока больше нет доступных упражнений
          </Typography>
        )}
      </Box>
    </Modal>
  );
};

export default ExerciseModal;
