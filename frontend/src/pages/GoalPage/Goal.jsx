import {
  Box,
  Button,
  Stack,
  styled,
  TextField,
  Typography,
} from "@mui/material";
import ArrowBackIcon from "@mui/icons-material/ArrowBack";
import { DatePicker } from "@mui/x-date-pickers/DatePicker";
import dayjs from "dayjs";
import WorkoutsFragment from "./Workouts/WorkoutsFragment";
import { useContext, useEffect, useState } from "react";
import Diet from "./Diets/Diet";
import { getWorkouts } from "../../api/workoutService";
import { getDiets } from "../../api/dietService";
import AuthContext from "../../store/AuthContext";
import DisabledTextField from "../../components/DisabledTextField/DisabledTextField";
import Workout from "./Workouts/Wokout";
import Timer from "./Timer";
import { BorderAllRounded, Height } from "@mui/icons-material";
import {
  getWorkoutEvents,
  getWorkoutEventsByGoalId,
} from "../../api/workoutEventJournalService";
import {
  getDietEvents,
  getDietEventsByGoalId,
} from "../../api/dietEventJournalService";

const CustomDatePicker = styled(DatePicker)(({ theme }) => ({
  "& .Mui-disabled": {
    opacity: 1, // убираем прозрачность
    WebkitTextFillColor: "rgba(0, 0, 0, 1)",
  },
  "& .MuiInputBase-root.Mui-disabled": {
    opacity: 1, // убираем прозрачность
    WebkitTextFillColor: "rgba(0, 0, 0, 1)",
  },
  "&.MuiInputBase-input-MuiOutlinedInput-input .Mui-disabled": {
    opacity: 1, // убираем прозрачность
    WebkitTextFillColor: "rgba(0, 0, 0, 1)",
  },
}));

const Goal = ({ goal, onClickBack }) => {
  const { token, email } = useContext(AuthContext);
  const [diets, setDiets] = useState([]);
  const [workouts, setWorkouts] = useState([]);
  const [totalBurned, setTotalBurned] = useState(0);
  const [totalGained, setTotalGained] = useState(0);

  const getWorkoutsAndDietsByGoalId = async (id) => {
    try {
      setWorkouts(await getWorkouts(token, id));
      setDiets(await getDiets(token, id));
    } catch (err) {
      console.log(err);
    }
  };

  const getTotalCalories = async () => {
    try {
      setTotalBurned(
        (await getWorkoutEventsByGoalId(token, email, goal.id)).reduce(
          (sum, item) => (sum += Number(item.burnedCalories)),
          0
        )
      );
      setTotalGained(
        (await getDietEventsByGoalId(token, email, goal.id)).reduce(
          (sum, item) => (sum += Number(item.calories)),
          0
        )
      );
    } catch (err) {
      console.log(err);
    }
  };

  useEffect(() => {
    getWorkoutsAndDietsByGoalId(goal?.id);
    getTotalCalories();
  }, []);

  return (
    <>
      <Stack direction="row" sx={{ marginBottom: 4 }}>
        <Button
          variant="contained"
          startIcon={<ArrowBackIcon />}
          onClick={onClickBack}
        >
          Назад
        </Button>
      </Stack>
      <Stack direction="row">
        <Stack>
          <Stack direction="row" sx={{ gap: 2 }}>
            <DisabledTextField disabled label="Название" value={goal.name} />
            <CustomDatePicker
              disabled
              value={dayjs(goal.deadline)}
              label="Дедлайн"
            />
          </Stack>
          <DisabledTextField
            disabled
            label="Описание"
            multiline
            rows={8}
            value={goal.description}
            sx={{ minWidth: 500, maxWidth: 500, marginTop: 3 }}
          />
        </Stack>
        <Stack
          sx={{ margin: "0 auto" }}
          alignItems="center"
          justifyContent="center"
        >
          <Typography variant="h5">Всего калорий</Typography>
          <Box
            sx={{
              display: "flex",
              justifyContent: "center",
              flexDirection: "column",
              alignItems: "center",
              gap: 2,
              width: 200,
              height: 200,
              border: "10px solid #66bb6a",
              borderRadius: "50%",
            }}
          >
            <Typography>Набрано: {totalGained.toFixed(0)}</Typography>
            <Typography>Сожжено: {totalBurned.toFixed(0)}</Typography>
          </Box>
        </Stack>
      </Stack>
      <Stack sx={{ marginTop: 4 }}>
        <Stack sx={{ gap: 2 }}>
          <Typography sx={{ fontSize: 20 }}>Диеты</Typography>
          {diets.length !== 0 ? (
            <Stack sx={{ gap: 4 }}>
              {diets.map((diet, index) => (
                <Diet
                  disabled={true}
                  key={index}
                  id={diet.id}
                  name={diet.name}
                  addedProducts={diet?.products ?? []}
                  addedDishes={diet?.dishes ?? []}
                />
              ))}
            </Stack>
          ) : (
            <Typography>В данной цели пока нет диет</Typography>
          )}
        </Stack>
        <Stack sx={{ gap: 2, marginTop: 8 }}>
          <Typography sx={{ fontSize: 20 }}>Тренировки</Typography>
          {workouts.length !== 0 ? (
            <Stack sx={{ gap: 4 }}>
              {workouts.map((workout, index) => (
                <Workout
                  disabled={true}
                  key={index}
                  id={workout.id}
                  name={workout.name}
                  addedExercises={workout?.exercises ?? []}
                />
              ))}
            </Stack>
          ) : (
            <Typography>В данной цели пока нет тренировок</Typography>
          )}
        </Stack>
      </Stack>
    </>
  );
};

export default Goal;
