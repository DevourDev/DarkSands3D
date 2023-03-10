﻿namespace Game.Abilities
{
    public sealed class KillingSpheresAbility : AbilitySo
    {
        //для применения необходимо наличие необходимого количества динамических статов (далее - мана)
        //в течение 1 секунды подготавливает заклинание (подготовку может отменить сам кастер либо другой
        //персонаж с помощью оглушения\наложения безмолвия\лишения маны кастера)
        //после успешного завершения подготовки начинается применение заклинания:
        //тратится мана
        //3 секунды вокруг персонажа крутится шар, постепенно увеличивая свой радиус
        //шар наносит урон вражеским персонажам при касании (чтобы шар А нанёс урон юниту Б
        //2 раза, шар должен совершить полный оборот вокруг кастера (чтобы шар не наносил
        //урон 99 раз убегающему юниту))
        //спустя 3 секунды появляются новые шары, но уже каждую секунду.
        //новые шары наносят другое количество урона
        //новые шары уничтожаются после нанесения определенного
        //количества урона (шар В нанёс 10 раз по 100 урона, лимит - 1001 урона,
        //на 11 раз шар нанесёт урон (100, не 1) и лопнет
        //первый шар не имеет лимита наносимого урона
        //на призыв каждого шара (кроме первого) требуется мана
        //максимальное количество дополнительных шаров огранчено
        //способность прерывается, если лимит шаров не достигнут
        //            и новый шар не может появиться (из-за маны)
        //            в течение 3 секунд
        //время действия способности:
        //максимальное - 15 секунд
        //минимальное (начиная отсчет с призыва первого шара) - 6 секунды:
        //            через 3 секунды не должно хватить маны на призыв шарика
        //            и следующие 3 секунды - тоже
        //кастер может отменить способность:
        //            в таком случае новые шары не будут появляться даже, если
        //            на них хватает маны. Если лимит шаров достигнут и шары
        //            не лопаются - они будут крутиться до окончания действия
        //            основной способности (15 секунд)

        public override void Use(AbilityContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}