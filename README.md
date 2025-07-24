
# A modern boilerplate for integrating with REST APIs!

This project already has everything you need to start with. It will boost your integration seamlessly, as a sample API has been implemented using the best practices:

1- It uses the IHttpClientFactory interface to instantiate the HttpClient class.
2- To respect the Single Responsibility Principle, I split my logic across two services:

> MarketStackClientService: Responsible only for calling the external API (MarketStack) and returning the result as a result-pattern object.

> MarketStackService: Orchestrates the business flow. It calls MarketStackClientService, checks the result, and reacts—whether that’s saving to the DB, calling another service, or just logging a failure.  

3- Bonus Tip: Equipped with a general Logging DelegatingHandler

If you've been using HttpClient the old-school way, it’s probably time to level up ⚙️💡

Your can learn more in my post on [LinkedIn](https://www.linkedin.com/posts/k-ebinj_httpclient-best-practices-part-1-activity-7352207534088159232-0TNg)
