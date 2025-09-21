import { createContext, useState } from "react";
const PdfContext = createContext();

export const PdfProvider = ({ children }) => {
  const [diets, setDiets] = useState([]);
  const [dietEvents, setDietEvents] = useState([]);
  const [workouts, setWorkouts] = useState([]);
  const [workoutEvents, setWorkoutEvents] = useState([]);

  const handleSetDiets = (val) => setDiets(val);
  const handleSetDietEvents = (val) => setDietEvents(val);
  const handleSetWorkouts = (val) => setWorkouts(val);
  const handleSetWorkoutEvents = (val) => setWorkoutEvents(val);
  return (
    <PdfContext.Provider
      value={{
        diets,
        dietEvents,
        workouts,
        workoutEvents,
        setD: handleSetDiets,
        setDE: handleSetDietEvents,
        setW: handleSetWorkouts,
        setWE: handleSetWorkoutEvents,
      }}
    >
      {children}
    </PdfContext.Provider>
  );
};

export default PdfContext;
