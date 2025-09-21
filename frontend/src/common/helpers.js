import dayjs from "dayjs";

export const getFormatedDate = (date) =>
  dayjs(date).format("YYYY-MM-DD").toString();
