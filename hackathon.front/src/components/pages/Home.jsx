import { Component } from 'react'
import gogol from '../../assets/gogol.png'
import histpic from '../../assets/histpic.png'
import Search from './search/Search'
import BuyImg from '../../assets/buysec.png'
import ShopButton from './shop/ShopBtn'
import BookSection from './book/BookSection'
import Blog from './blog/Blog'
import SearchByTitle from './search/SearchByTitle'
import SearchByDescription from './search/SearchByDesc'
 

export class Home extends Component {
    render() {
        return (
            <div className='home'>
                <div className='main'>
                    <div className='main-wrapper'>
                        <div className='main-left'>
                            <img src={gogol} alt='' className='main-left-pic' />
                        </div>
                        <div className='main-right'>
                            <h1 className='head1'>НИКОЛАЙ ГОГОЛЬ!</h1>
                            <h1 className='head2'>ВЕЛИКАЯ ПЕСНЬ</h1>
                            <h1 className='head3'>ОСВОБОЖДЕНИЯ!</h1>
                            <p className='text1'>Духовный учитель всего мира<br />
                                Его Божественная Милость Абхай Чаранаравинда<br /> Шрила Бхактиведанта Свами Прабхупада</p>
                        </div>
                    </div>
                </div>
                <div className='hist'>
                    <div className='hist-wrapper'>
                        <div className='hist-parts'>
                            <div className='hist-left'>
                                <div className='hist-left-text1'>
                                    <p>Сюжет повести «Невский проспект» Николая<br /> Гоголя разворачивается вокруг двух приятелей, каждый из которых<br /> устремляется за молодой незнакомой девушкой: Пирогов — за<br /> блондинкой, а Пискарев — за брюнеткой. Истории этих молодых<br /> людей заканчиваются по-разному: одна — трагически, во второй<br /> герой тоже терпит неудачу, но она больше похожа на комедию.<br /> Завершается повесть описанием Невского проспекта, где<br /> писатель видит одну только ложь и отсутствие смысла.
                                    </p>
                                </div>
                                <div className='hist-left-text2'>
                                    <p>Поблагодарите бога прежде всего за<br /> то, что вы русскойля всех людей</p>
                                </div>
                            </div>
                            <div className='hist-right'>
                                <div className='hist-right-pic'>
                                    <img src={histpic} alt='' className='hist-right-pic1' />
                                </div>
                                <div className='hist-right-text3'>
                                    <h1>
                                        Как внешняя<br /> красота<br /> обманывает<br /> людей?
                                    </h1>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div className='booksrc'>
                    <div className='booksrc-wrapper'>
                        <div className='booksrc-parts'>
                            <div className='booksrc-top'>
                                <div className='booksrc-field'><SearchByTitle /></div>
                                <div className='booksrc-field'><SearchByDescription /></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div className='buy'>
                    <div className='buy-wrapper'>
                        <div className='buy-parts'>
                            <div className='buy-top'>
                                <h1>КТО МЫ?</h1>
                                <p className='buy-p'>Карагандинская областная универсальная научная библиотека имени Н.В. Гоголя, основанная в Караганде, является ведущим культурным и научным учреждением региона. Расположенная по адресу: ул. Ерубаева, д. 44, она предоставляет широкий спектр услуг, включая доступ к книгам, научным публикациям и культурным мероприятиям. Библиотека регулярно проводит выставки, лекции, литературные вечера и другие мероприятия, направленные на популяризацию знаний и развитие культуры.</p>
                            </div>
                            <div className='buy-mid'>
                                <div className='buy-img'>
                                    <img src={BuyImg} alt='' />
                                </div>
                                <div className='buy-info'>
                                    <h1>Шри Намамрита:<br />
                                        нектар святого имени</h1>
                                    <p className='buy-p'>Его божественная Милость<br />
                                        А. Ч. Бхактиведанта Свами Прабхупада</p>
                                    <div className='buy-info-sec'>
                                        <ShopButton />
                                        <div className='buy-price'>450 р.</div>
                                    </div>
                                </div>
                            </div>
                            <div className='buy-btm'>
                                <p className='buy-p'>Она активно участвует в проектах, связанных с инклюзивным образованием и поддержкой людей с особыми потребностями. Руководителем библиотеки с октября 2023 года является Айнур Сакеновна Шаймуханбетова, специалист с многолетним опытом в библиотечном деле и государственной службе. Под ее руководством библиотека участвует в международных проектах, таких как меморандумы с российскими и казахстанскими учреждениями, направленные на развитие инклюзивного общества. Библиотека была награждена за создание лучшего инклюзивного онлайн-проекта и продолжает вносить значительный вклад в культурное развитие региона.</p>
                            </div>
                        </div>
                    </div>
                </div>
                <div>
                    <BookSection />
                </div>
                <div>
                    <Blog />
                </div>
            </div>
        )
    }
}

export default Home
