import { createContext, useState } from "react";

const WorkoutsContext = createContext();

export const WorkoutsProvider = ({ children }) => {
  const [workouts, setWorkouts] = useState([]);

  const handleDeleteWorkout = (id) => {
    setWorkouts(workouts.filter((w) => w.id !== id));
  };

  const handleAddWorkout = (workout) => {
    setWorkouts((prev) => [...prev, workout]);
  };

  const handleUpdateWorkoutName = (id, newName) => {
    setWorkouts(
      workouts.map((w) => (w.id === id ? { ...w, name: newName } : w))
    );
  };

  const handleReplaceExercisesToWorkout = (workoutId, exercises) => {
    console.log(exercises);
    setWorkouts(
      workouts.map((w) =>
        w.id === workoutId ? { ...w, exercises: exercises } : w
      )
    );
  };

  const handleGetById = (id) => workouts.find((w) => w.id === id);

  return (
    <WorkoutsContext.Provider
      value={{
        workouts,
        init: setWorkouts,
        del: handleDeleteWorkout,
        create: handleAddWorkout,
        addExercises: handleReplaceExercisesToWorkout,
        updateName: handleUpdateWorkoutName,
        getById: handleGetById,
      }}
    >
      {children}
    </WorkoutsContext.Provider>
  );
};

export default WorkoutsContext;
