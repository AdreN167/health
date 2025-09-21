import useApi from "../hooks/useApi";

export const getWorkoutEvents = async (token, email) => {
  const api = useApi(token);
  const response = await api.get("/api/WorkoutEventJournal/" + email);
  return response.data.data;
};

export const getWorkoutEventsByGoalId = async (token, email, goalId) => {
  const api = useApi(token);
  const response = await api.get(
    "/api/WorkoutEventJournal?email=" + email + "&goalId=" + goalId
  );
  return response.data.data;
};

export const createWorkoutEvent = async (token, body) => {
  const api = useApi(token);
  const response = await api.post("/api/WorkoutEventJournal", body);
  return response.data.data;
};
