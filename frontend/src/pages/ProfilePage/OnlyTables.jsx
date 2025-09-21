import {
  Box,
  Table,
  TableHead,
  TableBody,
  TableCell,
  TableRow,
  Typography,
} from "@mui/material";
import { getFormatedDate } from "../../common/helpers";
import dayjs from "dayjs";
import { useEffect, useState } from "react";

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

const OnlyTables = ({ diets, workouts }) => {
  const [totalGained, setTotalGained] = useState(0);
  const [totalBurned, setTotalBurned] = useState(0);

  useEffect(() => {
    setTotalGained(
      diets.reduce((sum, item) => (sum += Number(item.calories)), 0)
    );
    setTotalBurned(
      workouts.reduce((sum, item) => (sum += Number(item.burnedCalories)), 0)
    );
  }, [diets, workouts]);

  return (
    <Box
      sx={{
        mawWidth: 300,
      }}
    >
      <Typography variant="h5" sx={{ marginTop: 4, marginLeft: 2 }}>
        Всего набрано калорий: {totalGained.toFixed(2)}
      </Typography>
      <Typography variant="h5" sx={{ marginTop: 2, marginLeft: 2 }}>
        Всего сожжено калорий: {totalBurned.toFixed(2)}
      </Typography>
      <Typography
        variant="h5"
        sx={{ fontWeight: 600, height: 40, marginTop: 4, marginLeft: 2 }}
      >
        Журнал тренировок
      </Typography>
      <Table sx={{ marginTop: 2 }}>
        <TableHead>
          <TableRow>
            <TableCell>Id</TableCell>
            <TableCell>Дата</TableCell>
            <TableCell>Сожженные калории</TableCell>
            <TableCell>Тренировка</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {workouts.map((workout) => {
            return (
              <TableRow key={workout.id}>
                <TableCell>{workout.id}</TableCell>
                <TableCell>
                  {getFormatedDate(createDate(workout.date))}
                </TableCell>
                <TableCell>{workout.burnedCalories}</TableCell>
                <TableCell>{workout.workout}</TableCell>
              </TableRow>
            );
          })}
        </TableBody>
      </Table>
      <Typography
        variant="h5"
        sx={{ fontWeight: 600, height: 40, marginTop: 4, marginLeft: 2 }}
      >
        Журнал диет
      </Typography>
      <Table sx={{ marginTop: 2 }}>
        <TableHead>
          <TableRow>
            <TableCell>Id</TableCell>
            <TableCell>Дата</TableCell>
            <TableCell>Калории</TableCell>
            <TableCell>Белки</TableCell>
            <TableCell>Жиры</TableCell>
            <TableCell>Углеводы</TableCell>
            <TableCell>Диета</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {diets.map((meal) => {
            return (
              <TableRow key={meal.id}>
                <TableCell>{meal.id}</TableCell>
                <TableCell>{getFormatedDate(createDate(meal.date))}</TableCell>
                <TableCell>{meal.calories.toFixed(2)}</TableCell>
                <TableCell>{meal.proteins.toFixed(2)}</TableCell>
                <TableCell>{meal.fats.toFixed(2)}</TableCell>
                <TableCell>{meal.carbohydrates.toFixed(2)}</TableCell>
                <TableCell>{meal.diet}</TableCell>
              </TableRow>
            );
          })}
        </TableBody>
      </Table>
    </Box>
  );
};

export default OnlyTables;
