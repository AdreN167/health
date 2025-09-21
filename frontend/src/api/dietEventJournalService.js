import useApi from "../hooks/useApi";

export const getDietEvents = async (token, email) => {
  const api = useApi(token);
  const response = await api.get("/api/DietEventJournal/" + email);
  return response.data.data;
};

export const getDietEventsByGoalId = async (token, email, goalId) => {
  const api = useApi(token);
  const response = await api.get(
    "/api/DietEventJournal?email=" + email + "&goalId=" + goalId
  );
  return response.data.data;
};

export const createDietEvent = async (token, body) => {
  const api = useApi(token);
  const response = await api.post("/api/DietEventJournal", body);
  return response.data.data;
};
