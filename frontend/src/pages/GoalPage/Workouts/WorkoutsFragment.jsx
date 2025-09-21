import { IconButton, Stack, TextField, Typography } from "@mui/material";
import useInput from "../../../hooks/useInput";
import CreateButton from "../../../components/CreateButton/CreateButton";
import AddIcon from "@mui/icons-material/Add";
import DoneIcon from "@mui/icons-material/Done";
import { useContext, useEffect, useState } from "react";
import Workout from "./Wokout";
import { uid } from "react-uid";
import { random } from "@mui/x-data-grid-generator";
import WorkoutsContext from "../../../store/Workouts/WorkoutsContext";

const WorkoutsFragment = ({ sx }) => {
  const { workouts, updateName, create, getById, del } =
    useContext(WorkoutsContext);

  const [workoutInEdit, setWorkoutInEdit] = useState(null);
  const name = useInput("Новая тренировка", (str) =>
    str !== "" && str !== " " && str !== null
      ? { isValid: true, err: null }
      : { isValid: false, err: "Неверный ввод" }
  );

  useEffect(() => {
    if (!workoutInEdit) return;
    name.setValue(workoutInEdit.name);
  }, [workoutInEdit]);

  const handleSubmit = (e) => {
    e.preventDefault();

    if (workoutInEdit) {
      updateName(workoutInEdit.id, name.value);
      setWorkoutInEdit(null);
    } else {
      create({
        // это чисто для идентификации тренировок, которые только что были созданы, но не сохранены в БД
        id: uid(name.value + random(1, 10000000)),
        name: name.value,
        exercises: [],
      });
    }
    resetInputs();
  };

  const resetInputs = () => {
    name.reset();
  };

  const handleUpdateWorkout = (id) => {
    setWorkoutInEdit(getById(id));
  };

  const handleDeleteWorkout = (id) => {
    del(id);
  };

  return (
    <Stack sx={{ gap: 2, ...sx }}>
      <Typography sx={{ fontSize: 20 }}>Тренировки</Typography>
      <form onSubmit={handleSubmit}>
        <TextField
          label="Название тренировки"
          value={name.value}
          onChange={name.onchange}
          error={name.error !== null}
          helperText={name.error}
        />
        {workoutInEdit ? (
          <IconButton type="submit" sx={{ marginTop: 1 }}>
            <DoneIcon color="success" />
          </IconButton>
        ) : (
          <CreateButton sx={{ marginLeft: 2 }} type="submit">
            <AddIcon />
          </CreateButton>
        )}
      </form>
      {workouts.length !== 0 && (
        <Stack sx={{ gap: 4 }}>
          {workouts.map((workout, index) => (
            <Workout
              key={index}
              id={workout.id}
              name={workout.name}
              addedExercises={workout?.exercises ?? []}
              onUpdate={handleUpdateWorkout}
              onDelete={handleDeleteWorkout}
            />
          ))}
        </Stack>
      )}
    </Stack>
  );
};

export default WorkoutsFragment;
