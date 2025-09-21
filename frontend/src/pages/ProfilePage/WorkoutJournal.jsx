import { useContext, useEffect, useMemo, useState } from "react";
import {
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Paper,
  Select,
  MenuItem,
  InputLabel,
  FormControl,
  Button,
  Stack,
} from "@mui/material";
import { DatePicker } from "@mui/x-date-pickers/DatePicker";
import dayjs from "dayjs";
import { getDietEvents } from "../../api/dietEventJournalService";
import AuthContext from "../../store/AuthContext";
import { getFormatedDate } from "../../common/helpers";
import { getGoalsByUserEmail } from "../../api/goalService";
import { getDiets } from "../../api/dietService";
import { getWorkouts } from "../../api/workoutService";
import { getWorkoutEvents } from "../../api/workoutEventJournalService";
import PdfContext from "../../store/PdfContext";

const createDate = (date) =>
  date
    ? dayjs(date)
        .set("second", 0)
        .set("hour", 0)
        .set("minute", 0)
        .set("millisecond", 0)
    : dayjs()
        .set("second", 0)
        .set("hour", 0)
        .set("minute", 0)
        .set("millisecond", 0);

const WorkoutJournal = ({ sx, workoutsDate, workouts, getResult }) => {
  const { token, email } = useContext(AuthContext);
  const ctx = useContext(PdfContext);

  const [startDate, setStartDate] = useState(createDate().add(-1, "month"));
  const [endDate, setEndDate] = useState(createDate());
  const [selectedWorkout, setSelectedWorkout] = useState("");

  const filteredWorkouts = workoutsDate.filter((workout) => {
    const workoutDate = new Date(workout.date);
    const isWithinDateRange =
      (!startDate || workoutDate >= new Date(startDate)) &&
      (!endDate || workoutDate <= new Date(endDate));
    const isWorkoutMatch =
      !selectedWorkout || workout.workoutId === selectedWorkout;
    return isWithinDateRange && isWorkoutMatch;
  });

  const [sortConfig, setSortConfig] = useState({
    key: "date",
    direction: "descending",
  });
  const sortedData = useMemo(() => {
    let sortableItems = [...filteredWorkouts];
    if (sortConfig !== null) {
      sortableItems.sort((a, b) => {
        if (a[sortConfig.key] < b[sortConfig.key]) {
          return sortConfig.direction === "ascending" ? -1 : 1;
        }
        if (a[sortConfig.key] > b[sortConfig.key]) {
          return sortConfig.direction === "ascending" ? 1 : -1;
        }
        return 0;
      });
    }
    return sortableItems;
  }, [filteredWorkouts, sortConfig]);

  const requestSort = (key) => {
    let direction = "ascending";
    if (
      sortConfig &&
      sortConfig.key === key &&
      sortConfig.direction === "ascending"
    ) {
      direction = "descending";
    }
    setSortConfig({ key, direction });
  };

  useEffect(() => {
    getResult(
      sortedData.map((i) => ({
        ...i,
        workout: workouts.find((w) => w.id === i.workoutId)?.name,
      }))
    );
  }, [filteredWorkouts, sortConfig]);
  return (
    <Stack sx={{ ...sx }}>
      <Stack direction="row">
        <FormControl variant="outlined" style={{ width: 150 }}>
          <InputLabel>Тренировка</InputLabel>
          <Select
            value={selectedWorkout}
            onChange={(e) => setSelectedWorkout(e.target.value)}
            label="Диета"
          >
            <MenuItem value="">
              <em>Все</em>
            </MenuItem>
            {workouts.map((diet) => (
              <MenuItem key={diet.id} value={diet.id}>
                {diet.name}
              </MenuItem>
            ))}
          </Select>
        </FormControl>

        <DatePicker
          sx={{ maxWidth: 150, marginLeft: 2 }}
          value={startDate}
          onChange={(e) => setStartDate(e)}
          label="Дата начала"
        />
        <DatePicker
          sx={{ maxWidth: 150, marginLeft: 2 }}
          value={endDate}
          onChange={(e) => setEndDate(e)}
          label="Дата конца"
        />

        <Button
          variant="contained"
          color="primary"
          onClick={() => {
            setStartDate(createDate().add(-1, "month"));
            setEndDate(createDate());
            setSelectedWorkout("");
          }}
          sx={{
            marginLeft: 2,
          }}
        >
          Сбросить фильтры
        </Button>
      </Stack>

      <TableContainer component={Paper} style={{ marginTop: "20px" }}>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell
                sx={{
                  cursor: "pointer",
                  transition: "0.2s",
                  "&:hover": { bgcolor: "rgba(0, 0, 0, 0.1)" },
                }}
                onClick={() => requestSort("id")}
              >
                Id
              </TableCell>
              <TableCell
                sx={{
                  cursor: "pointer",
                  transition: "0.2s",
                  "&:hover": { bgcolor: "rgba(0, 0, 0, 0.1)" },
                }}
                onClick={() => requestSort("date")}
              >
                Дата
              </TableCell>
              <TableCell
                sx={{
                  cursor: "pointer",
                  transition: "0.2s",
                  "&:hover": { bgcolor: "rgba(0, 0, 0, 0.1)" },
                }}
                onClick={() => requestSort("burnedCalories")}
              >
                Сожженные калории
              </TableCell>
              <TableCell>Тренировка</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {sortedData.map((workout) => {
              return (
                <TableRow key={workout.id}>
                  <TableCell>{workout.id}</TableCell>
                  <TableCell>
                    {getFormatedDate(createDate(workout.date))}
                  </TableCell>
                  <TableCell>{workout.burnedCalories}</TableCell>
                  <TableCell>
                    {workouts.find((ad) => workout.workoutId === ad.id)?.name}
                  </TableCell>
                </TableRow>
              );
            })}
          </TableBody>
        </Table>
      </TableContainer>
    </Stack>
  );
};

export default WorkoutJournal;
