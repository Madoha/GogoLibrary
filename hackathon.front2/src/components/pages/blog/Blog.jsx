
const BlogSection = () => {
    const blogs = [
        {
            id: 1,
            title: 'Ура, у нас появилась своя онлайн платформа, спасибо 11 команде',
            image: 'https://avatars.mds.yandex.net/i?id=6d0affec8c265942739a7c21a695f4b7231fcc3efe8aba2d-5282144-images-thumbs&n=13',
        },
        {
            id: 2,
            title: 'Наша команда расширается, у нас новые работники которые горят желанием работать',
            image: 'https://avatars.mds.yandex.net/i?id=9d2d872dbaaecd4c9d46b1766b02553bbd20583b-4357367-images-thumbs&n=13',
        },
        {
            id: 3,
            title: 'Мы открыты, ждем вас всех у нас в библиотеке Н.В. Гоголя. г. Караганда',
            image: 'https://avatars.mds.yandex.net/i?id=7ce50f03ff6cc48464b52fb609a24f74a5a7efc0-10148308-images-thumbs&n=13',
        },
    ];

    return (
        <div className="blog-section">
            <div className="blog-wrapper">
                <h2>НАШ БЛОГ</h2>
                <div className="blog-cards">
                    {blogs.map((blog) => (
                        <div className="blog-card" key={blog.id}>
                            <img src={blog.image} alt={blog.title} className="blog-image" />
                            <div className="blog-content">
                                <p>{blog.title}</p>
                            </div>
                        </div>
                    ))}
                </div>
            </div>
        </div>
    );
};

export default BlogSection;
